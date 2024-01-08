<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Allergen extends Model
{
    use HasFactory;
    public $timestamps = false;

    public function ingredients():BelongsToMany
    {
        return $this->belongsToMany(Ingredient::class);
    }

}
