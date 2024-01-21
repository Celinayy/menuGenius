<?php

namespace App\Http\Controllers;

use App\Models\Purchase;
use App\Http\Requests\StorePurchaseRequest;
use App\Http\Requests\UpdatePurchaseRequest;
use \Illuminate\Database\Eloquent\ModelNotFoundException;

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
        if(auth()->check()) 
        {
            $user = auth()->user();
    
            if ($user->admin) 
            {
                $purchase = Purchase::with(['user', 'desk', 'products' => function ($query) 
                {
                    $query->withPivot('quantity');
                }])->find($id);
    
                if ($purchase) {
                    return response()->json($purchase, 200);
                } else {
                    return response()->json(['error' => 'Nincs ilyen vásárlás!'], 404);
                }
            } 
            else 
            {
                $purchase = $user->purchases()->with
                ([
                    'user', 'desk', 'products' => function ($query) 
                    {
                        $query->withPivot('quantity');
                    }
                ])->find($id);
    
                if ($purchase) {
                    return response()->json($purchase, 200);
                } else {
                    return response()->json(['error' => 'Nincs jogosultságod ehhez a vásárláshoz.'], 403);
                }
            }
        } 
        else 
        {
            return response()->json(['error' => 'Ismeretlen felhasználó!'], 401);
        }
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
    public function update(UpdatePurchaseRequest $request, $id)
    {
        $user = auth()->user();

        if (!$user) {
            return response()->json(['error' => 'Ismeretlen felhasználó!'], 401);
        }

        try{
            $purchase = Purchase::findOrFail($id);
        } catch (ModelNotFoundException $exception) {
            return response()->json(['error' => 'A vásárlás nem található.'], 404);
        }
    
        if ($user->admin || $user->id === $purchase->user_id) {
            if ($purchase->status == 'ordered' && $purchase->paid == false) {
                $purchase->update($request->only(['status', 'paid', 'desk_id']));
    
                if ($request->has('products')) {
                    $products = $request->input('products');
                    $purchase->products()->detach();

                    foreach ($products as $product) {
                        $quantity = $product['quantity'];
                        $syncData[$product['id']] = ['quantity' => $quantity];
                    }
                    
                    $purchase->products()->sync($syncData);
                    $purchase->recalculateTotalPay();
                }

                $updatedPurchase = $user->purchases()->with([
                    'user', 'desk', 'products' => function ($query) {
                        $query->withPivot('quantity');
                    }
                ])->find($id);
                
                return response()->json($updatedPurchase, 200);
            } else {
                return response()->json(['error' => 'A rendelést már nem lehet módosítani!']);
            }
        }
        return response()->json(['error' => 'Nincs jogosultság a módosításra.'], 403);
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(Purchase $purchase)
    {
        //
    }
}
