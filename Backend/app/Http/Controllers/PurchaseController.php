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
        $user = Auth::user();


        if($user -> admin == true)
        {
            $purchases = Purchase::with(['user', 'desk', 'products' => function ($query) {
                $query->withPivot('quantity');
            }])->get();
            return $purchases;

        }
        if($user -> admin == false)
        {
            $purchases = $user->purchases()->with
            ([
                'user', 'desk', 'products' => function ($query) 
                {
                    $query->withPivot('quantity');
                }
            ])->get();
            return $purchases;

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
            return response()->json(['error' => 'Nincs ilyen vásárlás!'], 404);
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
