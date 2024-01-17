<?php

namespace Database\Factories;

use Illuminate\Database\Eloquent\Factories\Factory;
use App\Models\User;
use App\Models\Desk;
use App\Models\Reservation;
use Faker\Provider\hu_HU\Person as HungarianPerson;


/**
 * @extends \Illuminate\Database\Eloquent\Factories\Factory<\App\Models\Reservation>
 */
class ReservationFactory extends Factory
{
    /**
     * Define the model's default state.
     *
     * @return array<string, mixed>
     */
    public function definition(): array
    {
        $this->faker->addProvider(new HungarianPerson($this->faker));
        $user = User::inRandomOrder()->first();
        $desk = Desk::inRandomOrder()->first();
        $name = $user->id == 3 ? $this->faker->name : $user->name;
        $number_of_guests = $this->faker->numberBetween(1, 20);
        $desk = Desk::where('number_of_seats', '>=', $number_of_guests)
                     ->orderByRaw('ABS(number_of_seats - ?)', [$number_of_guests])
                     ->first();
        return [
            'number_of_guests' => $number_of_guests,
            'checkin_date' => $this -> faker -> dateTimeBetween('-1 month', '+1 month'),
            'checkout_date' => function (array $attributes) {
                $hoursToAdd = $this->faker->numberBetween(1, 4);
                return $this->faker->dateTimeInInterval($attributes['checkin_date'], "+{$hoursToAdd} hours");
            },
            'name' => $name,
            'phone' => $this -> faker -> regexify('\+36 \d{2} \d{3} \d{4}'),
            'desk_id' => $desk -> id,
            'user_id' => $user -> id,
            'closed' => $this -> faker -> boolean,

        ];
    }

    public function configure(): Factory
    {
        return $this->afterCreating(function (Reservation $reservation) {
            if ($reservation->checkin_date < now()) {
                $reservation->closed = true;
                $reservation->save();
            }

            $hoursToAdd = $this->faker->numberBetween(1, 4);
            $reservation->checkout_date = $reservation->checkin_date->modify("+{$hoursToAdd} hours");
            $reservation->save();
        });
    }
}
