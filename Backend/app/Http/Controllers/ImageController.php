<?php

namespace App\Http\Controllers;

use App\Models\Image;
use App\Http\Requests\StoreImageRequest;
use App\Http\Requests\UpdateImageRequest;

class ImageController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        $images = Image::all();

        $encodedImages = $images->map(function ($image) {
            $base64 = base64_encode($image->img_data);

            return [
                'id' => $image->id,
                'img_name' => $image->img_name,
                'img_data' => $base64,
            ];
        });

        //return response()->json($encodedImages);
        return view('images.index', ['images' => $encodedImages]);
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
    public function store(StoreImageRequest $request)
    {
        $image = new Image();
        $image->img_name = $request->input('img_name');
        $image->img_data = $request->input('img_data');

        $image->save();

        return response()->json(['message' => 'Az asztal létrehozva!', 'data' => $image], 201);
    }

    /**
     * Display the specified resource.
     */
    public function show(Image $image)
    {
        if (!$image) {
            return response()->json(['error' => 'Nincs ilyen kép!'], 404);
        }
    
        return response()->json($image);
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(Image $image)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateImageRequest $request, Image $image)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(Image $image)
    {
        //
    }
}
