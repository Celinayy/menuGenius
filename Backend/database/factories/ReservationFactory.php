<?php

namespace Database\Factories;

use Illuminate\Database\Eloquent\Factories\Factory;
use App\Models\User;
use App\Models\Desk;
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
        $this->faker->locale('hu_HU');
        $user = User::inRandomOrder()->first();
        $desk = Desk::inRandomOrder()->first();

        return [
            'number_of_guests' => $this -> faker -> numberBetween(1, 20),
            'checkin_date' => $this -> faker -> dateTime(),
            'checkout_date' => $this -> faker -> dateTime(),
            'name' => $this->faker->name,
            'phone' => $this -> faker -> regexify('\+36 \d{2} \d{3} \d{4}'),
            'desk_id' => $desk -> id,
        ];
    }
}
