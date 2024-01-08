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
        Schema::create('event_logs', function (Blueprint $table) {
            $table->id();
            $table->string('event_type');
            $table->string('affected_table');
            $table->integer('affected_id');
            $table->string('event');
            $table->datetime('date');
            $table->unsignedBigInteger('user_id');
            //$table->timestamps();

            $table->foreign('user_id')->references("id")->on("users");

        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('event_logs');
    }
};
