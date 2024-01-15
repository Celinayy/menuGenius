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
        $products = DB::table('products')->inRandomOrder()->get();
        $purchases = DB::table('purchases')->get();

        foreach ($purchases as $purchase) {
            $productCount = rand(1, 5);

            $selectedProducts = $products->random($productCount);

            foreach ($selectedProducts as $product) {
                DB::table('product_purchase')->insert([
                    'quantity' => \Faker\Factory::create()->numberBetween(1, 3),
                    'product_id' => $product->id,
                    'purchase_id' => $purchase->id,
                ]);
            }
        }
    }
}
