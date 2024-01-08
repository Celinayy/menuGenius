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
        Schema::create('product_ingredient', function (Blueprint $table) {
            $table->unsignedbiginteger('product_id');
            $table->unsignedbiginteger('ingredient_id');
            //$table->timestamps();

            $table->foreign('product_id')->references('id')->on('products');
            $table->foreign('ingredient_id')->references('id')->on('ingredients');

        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('product_ingredient');
    }
};