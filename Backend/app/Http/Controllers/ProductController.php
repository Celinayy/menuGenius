<?php

namespace App\Http\Controllers;

use App\Models\Product;
use App\Http\Requests\StoreProductRequest;
use App\Http\Requests\UpdateProductRequest;

class ProductController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        return Product::with([
            'image', 
            'category', 
            'ingredients' => function($query) {
                $query->with('allergens');
            }
            ])->get();
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
    public function store(StoreProductRequest $request)
    {
        // $product = new Product();
        // $product->name = $request->input('name');
        // $product->description = $request->input('description');
        // $product->packing = $request->input('packing');
        // $product->price = $request->input('price');
        // $product->is_food = $request->input('is_food');
        // $product->category_id = $request->input('category_id');
        // $product->image_id = $request->input('image_id');

        // $product->save();

        // return response()->json(['message' => 'A termék létrehozva!', 'data' => $product], 201);
    }

    /**
     * Display the specified resource.
     */
    public function show($id)
    {
        return Product::with([
            'image', 
            'category', 
            'ingredients' => function($query) {
                $query->with('allergens');
            }
            ])->findOrFail($id);

        // if (!$product) {
        //     return response()->json(['error' => 'A terméket nem találtam!'], 404);
        // }

        // if($product){
        //     $product = Product::with([
        //         'image', 
        //         'category', 
        //         'ingredients' => function($query) {
        //             $query->with('allergens');
        //         }
        //     ])->findOrFail($product->id);
        //     return response()->json($product);
        // }
        //return response()->json($product);
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(Product $product)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateProductRequest $request, Product $product)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(Product $product)
    {
        //
    }
}
