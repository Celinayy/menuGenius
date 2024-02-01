<?php

namespace App\Http\Controllers;

use App\Models\Product;
use App\Http\Requests\StoreProductRequest;
use App\Http\Requests\UpdateProductRequest;
use Illuminate\Support\Facades\DB;


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
        //
    }

    /**
     * Display the specified resource.
     */
    public function show($id)
    {
        $product = Product::with([
            'image', 
            'category', 
            'ingredients' => function($query) {
                $query->with('allergens');
            }
        ])->find($id);
    
        if (!$product) {
            return response()->json(['error' => 'A terméket nem találtam!'], 404);
        }
    
        return response()->json($product);    }

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

    public function addToFavorites($productId)
    {
        if (auth()->check()) {
            $user = auth()->user();

            $user->products()->attach($productId, ['favorite' => true]);

            return response()->json(['message' => 'Termék hozzáadva a kedvencekhez']);
        } else {
            return response()->json(['message' => 'Be kell jelentkezni a kedvencekhez']);
        }
    }

    public function removeFromFavorites($productId)
    {
        if (auth()->check()) {
            $user = auth()->user();
    
            $pivotRow = $user->products()->where('product_id', $productId)->first();
    
            if ($pivotRow) {
                $user->products()->updateExistingPivot($productId, ['favorite' => false]);
    
                return response()->json(['message' => 'Termék eltávolítva a kedvencekből']);
            } else {
                return response()->json(['message' => 'A termék nem található a kedvencek között'], 404);
            }
        } else {
            return response()->json(['message' => 'Be kell jelentkezni a kedvencekhez']);
        }
    }

    public function userFavorites()
    {
        $user = auth()->user();
    
        $favorites = $user->products()
            ->wherePivot('favorite', true)
            ->with([
                'image', 
                'category', 
                'ingredients' => function($query) {
                    $query->with('allergens');
                }
            ])
            ->get();

        return response()->json(['favorites' => $favorites]);
    }
                
}
