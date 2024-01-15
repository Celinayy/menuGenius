<?php

namespace Database\Seeders;

// use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;


class DatabaseSeeder extends Seeder
{
    /**
     * Seed the application's database.
     */
    public function run(): void
    {
        DB::statement('SET FOREIGN_KEY_CHECKS=0;');

        DB::table('product_purchase')->truncate();
        DB::table('event_logs')->truncate();
        DB::table('reservations')->truncate();
        DB::table('purchases')->truncate();
        DB::table('users')->truncate();
        DB::table('desks')->truncate();
        DB::table('product_ingredient')->truncate();
        DB::table('products')->truncate();
        DB::table('categories')->truncate();
        DB::table('ingredient_allergen')->truncate();
        DB::table('ingredients')->truncate();
        DB::table('allergens')->truncate();

        DB::statement('SET FOREIGN_KEY_CHECKS=1;');

        $this->call(AllergenSeeder::class);
        $this->call(IngredientSeeder::class);
        $this->call(IngredientAllergenSeeder::class);
        $this->call(CategorySeeder::class);
        $this->call(ProductSeeder::class);
        $this->call(ProductIngredientSeeder::class);
        $this->call(DeskSeeder::class);
        $this->call(UserSeeder::class);
        $this->call(PurchaseSeeder::class);
        $this->call(ReservationSeeder::class);
        $this->call(EventLogSeeder::class);
        $this->call(ProductPurchaseSeeder::class);
        // \App\Models\User::factory(10)->create();

        // \App\Models\User::factory()->create([
        //     'name' => 'Test User',
        //     'email' => 'test@example.com',
        // ]);
    }
}
