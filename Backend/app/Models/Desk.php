<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\SoftDeletes;

class Desk extends Model
{
    use HasFactory;
    use SoftDeletes;
    public $timestamps = false;
    protected $dates = ['deleted_at'];

    public function reservation():HasMany
    {
        return $this->hasMany(Reservation::class);
    }

    public function purchase():HasMany
    {
        return $this->hasMany(Purchase::class);
    }

}
