<?php

namespace App\Http\Controllers;

use App\Models\Purchase;
use App\Http\Requests\StorePurchaseRequest;
use App\Http\Requests\UpdatePurchaseRequest;
use Illuminate\Support\Facades\Auth;

class PurchaseController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        // $purchase = Purchase::with(['user', 'desk', 'products'])->get();
        // //$purchase->load('product_purchase', 'quantity');
        // $formattedPurchases = $purchase->map(function ($purchase) {
        //     return [
        //         'purchase' => $purchase,
        //         'products' => $purchase->products,
        //     ];
        // });
        $user = Auth::user();

        if($user)
        {
            return Purchase::where('user_id', $user->id)->with(['user', 'desk', 'products'])->get();;
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
        $purchase = new Purchase();
        $purchase->date_time = $request->input('date_time');
        $purchase->total_pay = $request->input('total_pay');
        $purchase->status = $request->input('status');
        $purchase->paid = $request->input('paid');
        $purchase->user_id = $request->input('user_id');
        $purchase->desk_id = $request->input('desk_id');

        $purchase->save();

        return response()->json(['message' => 'A vásárlás létrehozva!', 'data' => $purchase], 201);
    }

    /**
     * Display the specified resource.
     */
    public function show(Purchase $purchase)
    {
        if (!$purchase) {
            return response()->json(['error' => 'Nincs ilyen vásrálás!'], 404);
        }
    
        return response()->json($purchase);
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
