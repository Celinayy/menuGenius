<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Purchase extends Model
{
    use HasFactory;
    public $timestamps = false;

    public function desk():HasOne
    {
        return $this->hasOne(Desk::class);
    }

    public function user():HasOne
    {
        return $this->hasOne(User::class);
    }

    public function products():BelongsToMany
    {
        return $this->belongsToMany(Product::class)->withPivot('quantity');
    }


}
