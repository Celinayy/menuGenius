<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Models\Product;
use Stripe\Stripe;
use Stripe\Checkout\Session;
use Illuminate\Support\Facades\Auth;
use App\Http\Requests\StorePurchaseRequest;
use App\Models\Purchase;
use Symfony\Component\HttpKernel\Exception\NotFoundHttpException;

class StripeController extends Controller
{
    public function index(Request $request)
    {
        return view(view: 'product.index');
    }

    public function checkout(Request $request)
    {
        Stripe::setApiKey(env('STRIPE_SECRET_KEY'));

        if (Auth::guard('sanctum')->check()) {
            $user = Auth::guard('sanctum')->user();
        } else {
            $user = null;
        }

        \Log::info('Customer', [$user]);


        $lineItems = [];
        $totalPrice = 0;

        foreach ($request->products as $productId) {
            $product = Product::findOrFail($productId);
            $totalPrice += $product->price;
            $lineItems[] = [
                'price_data' => [
                    'currency' => 'huf',
                    'unit_amount' => $product->price * 100,
                    'product_data' => [
                        'name' => $product->name,
                    ],
                ],
                'quantity' => 1,
            ];
        }

        $sessionData = [
            'payment_method_types' => ['card'],
            'line_items' => $lineItems,
            'mode' => 'payment',
            'success_url' => route('checkout.success',[], true).'?session_id={CHECKOUT_SESSION_ID}',
            'cancel_url' => route('checkout.cancel', [], true).'?session_id={CHECKOUT_SESSION_ID}',
            'customer_creation' => 'always'
        ];

        if ($user) {
            $sessionData['customer_email'] = $user->email;
        }

        $session = Session::create($sessionData);

        $purchase = new Purchase();
        $purchase->date_time = now()->format('Y-m-d H:i:s');
        $purchase->total_pay = $totalPrice;
        $purchase->status = 'ordered';
        $purchase->paid = false;
        $purchase->user_id = $user ? $user->id : null;
        $purchase->desk_id = $request->desk_id;
        $purchase->stripe_id = $session->id;

        $purchase->save();

        $products = $request->input('products');

        foreach ($products as $productId) {
            $product = Product::findOrFail($productId);

            $purchase->products()->attach($product, ['quantity' => 1]);
        }

        return [
            "url" => $session->url,
        ];

    }

    public function success(Request $request)
    {
        Stripe::setApiKey(env('STRIPE_SECRET_KEY'));
        $customer = null;
        try{
            $sessionId = $request->get('session_id');
            $session = Session::retrieve($sessionId);

            if (!$session){
                throw new NotFoundHttpException();
            }
            $customer = \Stripe\Customer::retrieve($session->customer, []);

            $purchase = Purchase::where('stripe_id', $session->id)->first();
            // echo '<pre>';
            // var_dump($purchase);
            // echo '</pre>';

            if (!$purchase){
                throw new NotFoundHttpException();
            }
            if ($purchase->paid === false){
                $purchase->paid = true;
                $purchase->save();
            }

            return view('product.checkout-success', compact('customer'));

        } catch(\Exception $e)
        {
            throw new NotFoundHttpException();
        }
    }

    public function cancel()
    {
        $purchase = Purchase::where('stripe_id', 'cs_test_b1xM2mc8XNxEZ81X85eobz4WkniDX6clDVBvYcIuyRzk6oUGRJOIazkokE')->first();
        echo '<pre>';
        var_dump($purchase);
        '</pre>';
    }

    public function webhook()
    {
        $endpoint_secret = env('STRIPE_WEBHOOK_SECRET');

        $payload = @file_get_contents('php://input');
        $sig_header = $_SERVER['HTTP_STRIPE_SIGNATURE'];
        $event = null;

        try {
            $event = \Stripe\Webhook::constructEvent(
                $payload, $sig_header, $endpoint_secret
            );
        } catch(\UnexpectedValueException $e) {
            return response('', 400);
        } catch(\Stripe\Exception\SignatureVerificationException $e) {
            return response('', 400);
        }

        switch ($event->type) {
            case 'checkout.session.completed':
                $session = $event->data->object;

                $purchase = Purchase::where('stripe_id', $session->id)->first();
                if ($purchase && $purchase->paid === false) {
                    $purchase->paid = true;
                    $purchase->save();
                    //E-mail küldése a vásárlónak
                }
            default:
                echo 'Ismeretlen eseménytípus ' . $event->type;
        }

        return response('');
    }
}
