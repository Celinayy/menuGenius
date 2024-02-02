<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\SoftDeletes;

class Allergen extends Model
{
    use HasFactory;
    use SoftDeletes;
    public $timestamps = false;
    protected $dates =['deleted_at'];

    public function ingredients():BelongsToMany
    {
        return $this->belongsToMany(Ingredient::class, 'ingredient_allergen')->as('ingredient_allergen');
    }

}
