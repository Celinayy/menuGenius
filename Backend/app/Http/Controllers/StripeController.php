<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Models\Product;
use Stripe\Stripe;
use Stripe\Checkout\Session;
use App\Http\Requests\StorePurchaseRequest;
use App\Models\Purchase;

class StripeController extends Controller
{
    public function index()
    {
        return view(view: 'index');
    }

    public function checkout(Request $request)
    {
        $user = $request->user();

        Stripe::setApiKey(config('stripe.sk'));
    
        foreach ($request->products as $productId) {
            $product = Product::findOrFail($productId);
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
            //'success_url' => route('success').'?session_id={CHECKOUT_SESSION_ID}',
            'success_url' => 'http://localhost:4200/payment/success?session_id={CHECKOUT_SESSION_ID}',
            'cancel_url' => route('index')
        ];
    
        if ($user) {
            $sessionData['customer_email'] = $user->email;
        }
    
        $session = Session::create($sessionData);
        //$successUrl = route('success', ['session_id' => $session->id]);
        //$sessionData['success_url'] = $successUrl;

        return redirect()->away($session->url);
    }

    public function success()
    {
        return view(view: 'index');
    }

    public function handleSuccess(Request $request)
    {
        $sessionId = $request->input('session_id');
    
        Stripe::setApiKey(config('stripe.sk'));
    
        try {
            $session = Session::retrieve($sessionId);
    
            if ($session->payment_status === 'paid') {
                $userId = $request->user() ? $request->user()->id : null;

                $purchase = new Purchase();
                $purchase -> date_time = $request->input('date_time');
                $purchase -> total_pay = $request->input('total_pay');
                $purchase -> status = $request->input('status');
                $purchase -> paid = $request->input('paid');
                $purchase -> desk_id = $request->input('desk_id');
                $purchase -> user_id = $userId;
                $purchase -> stripe_id = $request->input('stripe_id');
                $purchase -> products = $request->input('products');

                $purchase->save();

                return response()->json(['message' => 'A fizetés sikeres!'], 200);
            } else {
                return response()->json(['error' => 'A fizetés sikertelen!'], 400);
            }
        } catch (\Exception $e) {
            return response()->json(['error' => $e->getMessage()], 500);
        }
    }
}
