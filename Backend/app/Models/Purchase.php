<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;

class Purchase extends Model
{
    use HasFactory;

    protected $fillable = [
        'date_time',
        'total_pay',
        'status',
        'paid',
        'user_id',
        'desk_id',
    ];
    public $timestamps = false;

    public function desk():BelongsTo
    {
        return $this->belongsTo(Desk::class);
    }

    public function user():BelongsTo
    {
        return $this->belongsTo(User::class)->whereNotNull('id');
    }

    public function products():BelongsToMany
    {
        return $this->belongsToMany(Product::class, 'product_purchase')->as('product_purchase')->withPivot('quantity');
    }


}
