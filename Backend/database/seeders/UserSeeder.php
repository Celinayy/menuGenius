<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Support\Facades\Hash;
use Illuminate\Database\Seeder;
use App\Models\User;

class UserSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    protected static ?string $password;
    
    public function run(): void
    {

        \App\Models\User::create([
            'id' => 1,
            'name' => 'admin',
            'email' => 'menugenius@gmail.com',
            'email_verified_at' => now(),
            'password' => static::$password ??= Hash::make('password'),
            'phone' => '+36 30 123 4567',
            'admin' => true,
            'remember_token' => \Illuminate\Support\Str::random(10),
        ]);

        \App\Models\User::create([
            'id' => 2,
            'name' => 'user',
            'email' => 'user@gmail.com',
            'email_verified_at' => now(),
            'password' => static::$password ??= Hash::make('password'),
            'phone' => '+36 30 987 6543',
            'admin' => false,
            'remember_token' => \Illuminate\Support\Str::random(10),
        ]);

        \App\Models\User::create([
            'id' => 3,
            'name' => 'guest',
            'admin' => false,
        ]);


        User::factory(10)->create();
    }
}
