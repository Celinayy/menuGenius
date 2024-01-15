<?php

namespace App\Http\Controllers;

use App\Models\Desk;
use App\Http\Requests\StoreDeskRequest;
use App\Http\Requests\UpdateDeskRequest;

class DeskController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        return Desk::get();
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
    public function store(StoreDeskRequest $request)
    {
        // $desk = new Desk();
        // $desk->number_of_guests = $request->input('number_of_guests');

        // $desk->save();

        // return response()->json(['message' => 'Az asztal létrehozva!', 'data' => $desk], 201);
    }

    /**
     * Display the specified resource.
     */
    public function show(Desk $desk)
    {
        if (!$desk) {
            return response()->json(['error' => 'Nincs ilyen asztal!'], 404);
        }
    
        return response()->json($desk);
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(Desk $desk)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateDeskRequest $request, Desk $desk)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(Desk $desk)
    {
        //
    }
}
