<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class EventLog extends Model
{
    use HasFactory;
    public $timestamps = false;

    public function user():HasOne
    {
        return $this->hasOne(User::class);
    }
}
