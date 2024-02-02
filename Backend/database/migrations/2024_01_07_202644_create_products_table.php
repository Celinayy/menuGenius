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
        Schema::create('products', function (Blueprint $table) {
            $table->id();
            $table->string('name')->unique();
            $table->string('description');
            $table->unsignedBigInteger('category_id');
            $table->string('packing');
            $table->integer('price');
            $table->boolean('is_food');
            $table->unsignedBigInteger('image_id');
            $table->softDeletes();
            //$table->timestamps();

            $table->foreign('category_id')->references("id")->on("categories");
            $table->foreign('image_id')->references("id")->on("images");
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('products');
    }
};
