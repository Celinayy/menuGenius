<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Ingredient extends Model
{
    use HasFactory;
    public $timestamps = false;

    public function allergens():BelongsToMany
    {
        return $this->belongsToMany(Allergen::class);
    }

    public function products():BelongsToMany
    {
        return $this->belongsToMany(Product::class);
    }
}
