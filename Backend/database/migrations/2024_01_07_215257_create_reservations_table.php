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
            $table->id();
            $table->integer('number_of_guests');
            $table->datetime('checkin_date');
            $table->datetime('checkout_date');
            $table->string('name');
            $table->string('phone');
            $table->unsignedBigInteger('desk_id');
            $table->unsignedBigInteger('user_id')->nullable();
            $table->boolean('closed')->default(0);
            $table->string('comment')->nullable();
            $table->softDeletes();
            //$table->timestamps();

            $table->foreign('desk_id')->references("id")->on("desks")->onDelete('cascade');
            $table->foreign('user_id')->references("id")->on("users")->onDelete('cascade');

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
