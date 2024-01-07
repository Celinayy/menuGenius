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
        Schema::create('product', function (Blueprint $table) {
            $table->id('id');
            $table->string('name');
            $table->string('descripton');
            $table->integer('categoryId');
            $table->string('packing');
            $table->integer('price');
            $table->boolean('isFood');
            $table->integer('imageId');
            //$table->timestamps();

            $table->foreign('categoryId')->references("id")->on("user");
            $table->foreign('imageId')->references("imageId")->on("image");
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('product');
    }
};
