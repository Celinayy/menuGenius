<?php

namespace App\Http\Controllers;

use App\Models\Ingredient;
use App\Http\Requests\StoreIngredientRequest;
use App\Http\Requests\UpdateIngredientRequest;

class IngredientController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        return Ingredient::with(['allergens'])->get();
    }

    /**
     * Show the form for creating a new resource.
     */
    public function create()
    {
        //$this->authorize('create-ingredient', $ingredient);
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(StoreIngredientRequest $request)
    {
        $ingredient = new Ingredient();
        $ingredient->name = $request->input('name');

        $ingredient->save();

        return response()->json(['message' => 'A hozzávaló létrehozva!', 'data' => $ingredient], 201);
    }

    /**
     * Display the specified resource.
     */
    public function show(Ingredient $ingredient)
    {
        if (!$ingredient) {
            return response()->json(['error' => 'Nincs ilyen hozzávaló!'], 404);
        }
    
        return response()->json($ingredient);
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(Ingredient $ingredient)
    {
        //$this->authorize('edit-ingredient', $ingredient);
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateIngredientRequest $request, Ingredient $ingredient)
    {
        //$this->authorize('update-ingredient', $ingredient);
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(Ingredient $ingredient)
    {
        //$this->authorize('destroy-ingredient', $ingredient);
    }
}
