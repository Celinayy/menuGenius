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
        //
    }

    /**
     * Show the form for creating a new resource.
     */
    public function create()
    {
        $this->authorize('create-allergen', $allergen);
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(StoreAllergenRequest $request)
    {
        $this->authorize('store-allergen', $allergen);
    }

    /**
     * Display the specified resource.
     */
    public function show(Allergen $allergen)
    {
        $this->authorize('show-allergen', $allergen);
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(Allergen $allergen)
    {
        $this->authorize('edit-allergen', $allergen);
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateAllergenRequest $request, Allergen $allergen)
    {
        $this->authorize('update-allergen', $allergen);
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(Allergen $allergen)
    {
        $this->authorize('destroy-allergen', $allergen);
    }
}
