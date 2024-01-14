<?php

namespace App\Http\Controllers;

use App\Models\Category;
use App\Http\Requests\StoreCategoryRequest;
use App\Http\Requests\UpdateCategoryRequest;

class CategoryController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        return Category::get();
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
    public function store(StoreCategoryRequest $request)
    {
        // $category = new Category();
        // $category->name = $request->input('name');

        // $category->save();

        // return response()->json(['message' => 'A termékkategória létrehozva!', 'data' => $category], 201);
    }

    /**
     * Display the specified resource.
     */
    public function show(Category $category)
    {
        if (!$category) {
            return response()->json(['error' => 'Nincs ilyen kategória!'], 404);
        }
    
        return response()->json($category);
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(Category $category)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateCategoryRequest $request, Category $category)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(Category $category)
    {
        //
    }
}
