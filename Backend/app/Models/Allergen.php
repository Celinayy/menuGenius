<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;

class Allergen extends Model
{
    use HasFactory;
    public $timestamps = false;

    public function ingredients():BelongsToMany
    {
        return $this->belongsToMany(Ingredient::class, 'ingredient_allergen')->as('ingredient_allergen');
    }

}
