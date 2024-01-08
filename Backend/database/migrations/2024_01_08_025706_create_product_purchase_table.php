<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    /**
     * Run the migrations.
     */
    public function up(): void
    {
        Schema::create('product_purchase', function (Blueprint $table) {
            $table->id();
            $table->unsignedbiginteger('product_id');
            $table->unsignedbiginteger('purchase_id');
            $table->integer('quantity');
            //$table->timestamps();

            $table->foreign('product_id')->references('id')->on('products');
            $table->foreign('purchase_id')->references('id')->on('purchases');
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('product_purchase');
    }
};
