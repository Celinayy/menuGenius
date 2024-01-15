<?php

namespace App\Http\Controllers;

use App\Models\Allergen;
use App\Http\Requests\StoreAllergenRequest;
use App\Http\Requests\UpdateAllergenRequest;

class AllergenController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        return Allergen::get();
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
    public function store(StoreAllergenRequest $request)
    {
        // $allergen = new Allergen();
        // $allergen->code = $request->input('code');
        // $allergen->name = $request->input('name');

        // $allergen->save();

        // return response()->json(['message' => 'Az allergén létrehozva!', 'data' => $allergen], 201);
    }

    /**
     * Display the specified resource.
     */
    public function show(Allergen $allergen)
    {
        if (!$allergen) {
            return response()->json(['error' => 'Nincs ilyen allergén!'], 404);
        }
    
        return response()->json($allergen);
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(Allergen $allergen)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateAllergenRequest $request, Allergen $allergen)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(Allergen $allergen)
    {
        //
    }
}
