<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use App\Models\Product;
use Illuminate\Database\Eloquent\Factories\Factory;
use App\Models\Purchase;
use Illuminate\Support\Facades\DB;


class ProductPurchaseSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        for ($i = 0; $i < 100; $i++) {
            $productId = DB::table('products')->inRandomOrder()->value('id');
            DB::table('product_purchase')->insert([
                'quantity' => \Faker\Factory::create()->numberBetween(1, 3),
                'product_id' => $productId,
                'purchase_id' => Purchase::factory()->create()->id,
            ]);
        }
    }
}
