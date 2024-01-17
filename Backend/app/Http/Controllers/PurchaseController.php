<?php

namespace App\Http\Controllers;

use App\Models\Purchase;
use App\Http\Requests\StorePurchaseRequest;
use App\Http\Requests\UpdatePurchaseRequest;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Log;

class PurchaseController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        if(auth()->check()) 
        {
            $user = auth()->user();

            if ($user->admin == true)
            {
                $purchases = Purchase::with(['user', 'desk', 'products' => function ($query) 
                {
                    $query->withPivot('quantity');
                }])->get();

                return response()->json($purchases, 200);
            }
            else
            {
                $purchases = $user->purchases()->with
                ([
                    'user', 'desk', 'products' => function ($query) 
                    {
                        $query->withPivot('quantity');
                    }
                ])->get();
        
                return response()->json($purchases, 200);
            }
        }
        else
        {
            return response()->json(['error' => 'Ismeretlen felhasználó!'], 401);
        }
    

        //return Purchase::with(['users', 'desks', 'products'])->get();
    }

    /**
     * Show the form for creating a new resource.
     */
    public function create()
    {
        //
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(StorePurchaseRequest $request)
    {
        $purchase = Purchase::create([
            'date_time' => $request->input('date_time', now()),
            'total_pay' => $request->input('total_pay'),
            'status' => $request->input('status', 'ordered'),
            'paid' => $request->input('paid', false),
            'desk_id' => $request->input('desk_id'),
            'user_id' => $request->input('user_id')
            //'user_id' => auth()->id(),
        ]);
    
        $products = $request->input('products');
        foreach ($products as $product) {
            $purchase->products()->attach($product['id'], ['quantity' => $product['quantity']]);
        }

        return response()->json(['message' => 'A vásárlás létrehozva!', 'data' => $purchase], 201);
    }

    /**
     * Display the specified resource.
     */
    public function show($id)
    {
        if (!$id) {
            return response()->json(['error' => 'Nincs ilyen vásárlás!'], 404);
        }
        return response()->json($id);
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(Purchase $purchase)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdatePurchaseRequest $request, Purchase $purchase)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(Purchase $purchase)
    {
        //
    }
}
