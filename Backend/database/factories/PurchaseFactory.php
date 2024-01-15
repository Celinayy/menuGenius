<?php

namespace Database\Factories;

use Illuminate\Database\Eloquent\Factories\Factory;
use App\Models\User;
use App\Models\Desk;
use Illuminate\Support\Arr;


/**
 * @extends \Illuminate\Database\Eloquent\Factories\Factory<\App\Models\Purchase>
 */
class PurchaseFactory extends Factory
{
    /**
     * Define the model's default state.
     *
     * @return array<string, mixed>
     */
    public function definition(): array
    {
        $this->faker->locale('hu_HU');
        $statuses = ['ordered', 'cooked', 'served'];

        $user = $this->faker->boolean ? User::inRandomOrder()->first() : null;
        $desk = Desk::inRandomOrder()->first();

        return [
            'date_time' => $this -> faker -> dateTime(),
            'total_pay' => $this -> faker -> numberBetween(10000, 50000),
            'status' => Arr::random($statuses),
            'paid' => $this -> faker -> boolean,
            'user_id' => $user ? $user -> id : null,
            'desk_id' => $desk -> id,
        ];
    }
}
