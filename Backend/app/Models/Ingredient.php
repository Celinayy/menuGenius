<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\SoftDeletes;

class Ingredient extends Model
{
    use HasFactory;
    use SoftDeletes;
    public $timestamps = false;
    protected $dates = ['deleted_at'];

    public function allergens():BelongsToMany
    {
        return $this->belongsToMany(Allergen::class, 'ingredient_allergen')->as('ingredient_allergen');
    }

    public function products():BelongsToMany
    {
        return $this->belongsToMany(Product::class, 'product_ingredient')->as('product_ingredient');
    }
}
