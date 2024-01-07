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
        $this->authorize('create', Allergen::class);
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(StoreAllergenRequest $request)
    {
        $this->authorize('store', Allergen::class);
    }

    /**
     * Display the specified resource.
     */
    public function show(Allergen $allergen)
    {
        $this->authorize('show', Allergen::class);
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(Allergen $allergen)
    {
        $this->authorize('edit', Allergen::class);
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateAllergenRequest $request, Allergen $allergen)
    {
        $this->authorize('update', Allergen::class);
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(Allergen $allergen)
    {
        $this->authorize('destroy', Allergen::class);
    }
}
