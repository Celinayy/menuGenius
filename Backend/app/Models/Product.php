<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\HasOne;



class Product extends Model
{
    use HasFactory;
    public $timestamps = false;

    protected $fillable = [
        'product_id'
        ];


    public function category():BelongsTo
    {
        return $this->belongsTo(Category::class);
    }    
    
    public function image():BelongsTo
    {
        return $this->BelongsTo(Image::class);
    }

    public function ingredients():BelongsToMany
    {
        return $this->belongsToMany(Ingredient::class, 'product_ingredient')->as('product_ingredient');
    }

    public function purchases():BelongsToMany
    {
        return $this->belongsToMany(Ingredient::class, 'product_purchase')->withPivot('quantity')->as('product_purchase');
    }

    // public function allergens()
    // {
    //     return $this->ingredients()->belongsToMany(Allergen::class, 'ingredient_allergen');
    // }

}
