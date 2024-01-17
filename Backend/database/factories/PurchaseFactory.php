<?php

namespace Database\Factories;

use Illuminate\Database\Eloquent\Factories\Factory;
use App\Models\User;
use App\Models\Desk;
use App\Models\Purchase;
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
        $statuses = ['ordered', 'cooked', 'served'];

        //$user = $this->faker->boolean ? User::inRandomOrder()->first() : null;
        $user = User::inRandomOrder()->first();
        $desk = Desk::inRandomOrder()->first();

        return [
            'date_time' => $this -> faker -> dateTimeBetween('-2 week', 'now'),
            'total_pay' => $this -> faker -> numberBetween(10000, 50000),
            'status' => Arr::random($statuses),
            'paid' => $this -> faker -> boolean,
            'user_id' => $user -> id,
            'desk_id' => $desk -> id,
        ];
    }

    public function configure(): Factory
    {
        return $this->afterCreating(function (Purchase $purchase) {
            $twentyFourHoursAgo = now()->subHours(24);

            if ($purchase->date_time < $twentyFourHoursAgo) {
                $purchase->paid = true;
                $purchase->status = 'served';
                $purchase->save();
            }
        });
    }

}
