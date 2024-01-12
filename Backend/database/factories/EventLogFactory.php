<?php

namespace Database\Factories;

use Illuminate\Database\Eloquent\Factories\Factory;
use App\Models\User;
use Illuminate\Support\Facades\DB;
use Illuminate\Support\Arr;

/**
 * @extends \Illuminate\Database\Eloquent\Factories\Factory<\App\Models\EventLog>
 */
class EventLogFactory extends Factory
{
    /**
     * Define the model's default state.
     *
     * @return array<string, mixed>
     */
    public function definition(): array
    {
        $this->faker->locale('hu_HU');
        $events = ['beszúrás', 'módosítás', 'törlés'];

        $user = User::inRandomOrder()->first();
        $tableNames = ['allergens', 'categories', 'desks', 'images', 'ingredients', 'purchases', 'reservations', 'users', 'products'];
        $tableName = $this->faker->randomElement($tableNames);
        $minId = DB::table($tableName)->min('id');
        $maxId = DB::table($tableName)->max('id');


        return [
            'event_type' => Arr::random($events),
            'affected_table' =>  $this -> faker -> randomElement($tableNames),
            'affected_id' =>  $this -> faker -> numberBetween($minId, $maxId),
            'event' =>  $this -> faker -> paragraph($nbsentences=1, $variableNbSentences = true),
            'date' =>  $this -> faker -> dateTime(),
            'user_id' =>  $user -> id,
        ];
    }
}
