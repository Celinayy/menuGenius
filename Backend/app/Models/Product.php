<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Product extends Model
{
    use HasFactory;
    public $table = "product";
    public $timestamps = false;

    public function category():HasOne
    {
        return $this->hasOne(Category::class);
    }
    
    public function image():HasOne
    {
        return $this->hasOne(Image::class);
    }

    public function ingredients()
    {
        return $this->belongsToMany(Ingredient::class, 'product_ingredient', 'productId', 'ingredientId');
    }
}
