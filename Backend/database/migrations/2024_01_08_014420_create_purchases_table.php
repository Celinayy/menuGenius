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
        Schema::create('purchases', function (Blueprint $table) {
            $table->id();
            $table->datetime('date_time');
            $table->integer('total_pay');
            $table->enum('status',['ordered', 'cooked', 'served']);
            $table->boolean('paid');
            $table->unsignedBigInteger('user_id')->nullable();
            $table->unsignedBigInteger('desk_id');
            $table->string('stripe_id');
            $table->softDeletes();
            //$table->timestamps();

            $table->foreign('user_id')->references("id")->on("users")->onDelete('cascade');
            $table->foreign('desk_id')->references("id")->on("desks")->onDelete('cascade');

        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('purchases');
    }
};
