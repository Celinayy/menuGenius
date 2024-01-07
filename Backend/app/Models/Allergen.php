<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Allergen extends Model
{
    use HasFactory;
    public $table = "allergen";
    public $timestamps = false;

    public function ingredient_allergen():BelongsToMany
    {
        return $this->belongsToMany(Ingredient::class, 'ingredient_allergen', 'allergenId', 'ingredientId');
    }

}
