<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\SoftDeletes;

class EventLog extends Model
{
    use HasFactory;
    use SoftDeletes;
    public $timestamps = false;
    protected $dates = ['deleted_at'];

    public function user():BelongsTo
    {
        return $this->belongsTo(User::class);
    }
}
