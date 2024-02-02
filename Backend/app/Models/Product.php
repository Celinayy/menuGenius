<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\HasOne;
use Illuminate\Database\Eloquent\SoftDeletes;


class Product extends Model
{
    use HasFactory;
    use SoftDeletes;
    public $timestamps = false;

    protected $fillable = [
        'product_id'
        ];
    protected $dates = ['deleted_at'];


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
        return $this->belongsToMany(Purchase::class, 'product_purchase')->withPivot('quantity')->as('product_purchase');
    }

    public function users():BelongsToMany
    {
        return  $this->belongsToMany(User::class,'product_user')->as('product_user')->withPivot('favorite', 'stars');
    }
}
