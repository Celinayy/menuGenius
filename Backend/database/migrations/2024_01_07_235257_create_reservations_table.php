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
        Schema::create('reservations', function (Blueprint $table) {
            $table->id('id');
            $table->integer('numberOfGuests');
            $table->dateTime('checkInDate');
            $table->dateTime('checkOutDate');
            $table->string('name');
            $table->string('phone');
            $table->integer('desk_id');
            //$table->timestamps();

            $table->foreign('desk_id')->references("id")->on("desks");

        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('reservations');
    }
};
