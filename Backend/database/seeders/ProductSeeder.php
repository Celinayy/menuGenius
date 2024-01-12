<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use App\Models\Product;


class ProductSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        Product::create( [
            'id'=>1,
            'name'=>'Töltött tojás',
            'description'=>'Tejfölös-mustáros krémmel töltött főtt tojás.',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>69,
            'category_id'=>1
            ] );
            
            
                        
            Product::create( [
            'id'=>2,
            'name'=>'Bruschetta',
            'description'=>'Olívaolajjal, sóval, borssal ízesített pirított bagett hagymával és paradicsommal.',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>5,
            'category_id'=>1
            ] );
            
            
                        
            Product::create( [
            'id'=>3,
            'name'=>'Tepertős pogácsa',
            'description'=>'Omlós, kerek sült tészta töpörtyűvel.',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>66,
            'category_id'=>1
            ] );
            
            
                        
            Product::create( [
            'id'=>4,
            'name'=>'Körözött pirítós kenyérrel',
            'description'=>'Pirospaprikával és köménymaggal elkevert túrókrém pirítós kenyéren.',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>37,
            'category_id'=>1
            ] );
            
            
                        
            Product::create( [
            'id'=>5,
            'name'=>'Fokhagymakrém leves',
            'description'=>'Tejszínes-fokhagymás krémleves sajttal és pirított zsemlekockával.',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>24,
            'category_id'=>2
            ] );
            
            
                        
            Product::create( [
            'id'=>6,
            'name'=>'Marha erőleves fridattó módra',
            'description'=>'Erőleves fridatto palacsintatésztával.',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>44,
            'category_id'=>2
            ] );
            
            
                        
            Product::create( [
            'id'=>7,
            'name'=>'Jókai bableves',
            'description'=>'Tejfölös, csülkös bableves csipetkével.',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>36,
            'category_id'=>2
            ] );
            
            
                        
            Product::create( [
            'id'=>8,
            'name'=>'Halászlé',
            'description'=>'Pontyból készült halászlé gyufatésztával, halikrával.',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>29,
            'category_id'=>2
            ] );
            
            
                        
            Product::create( [
            'id'=>9,
            'name'=>'Milánói sertésborda',
            'description'=>'Panírozott sertéskaraj milánói makarónival.',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>48,
            'category_id'=>3
            ] );
            
            
                        
            Product::create( [
            'id'=>10,
            'name'=>'Csikós tokány penne tésztával',
            'description'=>'Sertéslapockából készült tokány paprikás-paradicsomos szósszal, penne tésztával',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>16,
            'category_id'=>3
            ] );
            
            
                        
            Product::create( [
            'id'=>11,
            'name'=>'Cigánypecsenye',
            'description'=>'Sült sertéstarja szalonnacsíkkal.',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>10,
            'category_id'=>3
            ] );
            
            
                        
            Product::create( [
            'id'=>12,
            'name'=>'Szűzérmék párizsiasan',
            'description'=>'Sertés szűzpecsenye párizsi bundában.',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>63,
            'category_id'=>3
            ] );
            
            
                        
            Product::create( [
            'id'=>13,
            'name'=>'Fokhagymás sült oldalas',
            'description'=>'Sült sertésoldalas só, bors, fokhagyma pácban.',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>25,
            'category_id'=>3
            ] );
            
            
                        
            Product::create( [
            'id'=>14,
            'name'=>'Csirkepaprikás nokedlivel',
            'description'=>'Csirkecomb tejfölös pörköltlében.',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>17,
            'category_id'=>4
            ] );
            
            
                        
            Product::create( [
            'id'=>15,
            'name'=>'Sztroganov csirkemell',
            'description'=>'Vékonyra szeletelt csirkemell tejszínes-tejfölös mártásban gombával és uborkával.',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>62,
            'category_id'=>4
            ] );
            
            
                        
            Product::create( [
            'id'=>16,
            'name'=>'Sült kacsacomb lila káposztával , krumplipürével',
            'description'=>'Ropogósra sült kacsacomb párolt káposztával, krumplipürével.',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>59,
            'category_id'=>4
            ] );
            
            
                        
            Product::create( [
            'id'=>17,
            'name'=>'Cordon Bleu',
            'description'=>'Sajttal és sonkával töltött borjúcomb szeletek panírmorzsába forgatva.',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>12,
            'category_id'=>5
            ] );
            
            
                        
            Product::create( [
            'id'=>18,
            'name'=>'Bécsi szelet',
            'description'=>'Vékonyra kivert borjúszeletek panírmorzsában.',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>3,
            'category_id'=>5
            ] );
            
            
                        
            Product::create( [
            'id'=>19,
            'name'=>'Marhapörkölt',
            'description'=>'Pörkölt marhalábszárból.',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>45,
            'category_id'=>5
            ] );
            
            
                        
            Product::create( [
            'id'=>20,
            'name'=>'Bélszín steak',
            'description'=>'Sült bélszínszeletek vajjal.',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>4,
            'category_id'=>5
            ] );
            
            
                        
            Product::create( [
            'id'=>21,
            'name'=>'Fish and Chips',
            'description'=>'Sörtésztába forgatott harcsaszeletek sült hasábburgonyával és borsópürével.',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>23,
            'category_id'=>6
            ] );
            
            
                        
            Product::create( [
            'id'=>22,
            'name'=>'Sült lazacsteak',
            'description'=>'Sült lazacfilé kamillás narancsmártással.',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>60,
            'category_id'=>6
            ] );
            
            
                        
            Product::create( [
            'id'=>23,
            'name'=>'Sült pisztráng',
            'description'=>'Mandulás bundában sült pisztráng zelleres burgonyapürével',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>61,
            'category_id'=>6
            ] );
            
            
                        
            Product::create( [
            'id'=>24,
            'name'=>'Pestós tészta',
            'description'=>'Spagettitészta pestós öntettel',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>53,
            'category_id'=>7
            ] );
            
            
                        
            Product::create( [
            'id'=>25,
            'name'=>'Rakott édesburgonya fekete fokhagymával',
            'description'=>'Rakott tál édesburgonyával, sok sajttal, sütőben sütve.',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>55,
            'category_id'=>7
            ] );
            
            
                        
            Product::create( [
            'id'=>26,
            'name'=>'Hummuszos cukkinitekercs',
            'description'=>'Cukkinibe tekert hummusz sajttal sütőben sütve.',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>32,
            'category_id'=>7
            ] );
            
            
                        
            Product::create( [
            'id'=>27,
            'name'=>'Milánói makaróni',
            'description'=>'Sonkás-gombás makaróni tészta sok sajttal.',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>47,
            'category_id'=>8
            ] );
            
            
                        
            Product::create( [
            'id'=>28,
            'name'=>'Túrós csusza',
            'description'=>'Csuszatészta sok túróval és tejföllel, pirított szalonnával, sütve.',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>71,
            'category_id'=>8
            ] );
            
            
                        
            Product::create( [
            'id'=>29,
            'name'=>'Mac & Cheese',
            'description'=>'Sajtos-vajas szósszal kevert szarvacska tészta.',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>41,
            'category_id'=>8
            ] );
            
            
                        
            Product::create( [
            'id'=>30,
            'name'=>'Görögsaláta',
            'description'=>'Saláta uborkával, hagymával, paradicsommal, olajbogyóval, feta sajttal.',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>27,
            'category_id'=>9
            ] );
            
            
                        
            Product::create( [
            'id'=>31,
            'name'=>'Tojásos virslisaláta',
            'description'=>'Hagymás-virslis saláta tejfölös öntettel.',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>68,
            'category_id'=>9
            ] );
            
            
                        
            Product::create( [
            'id'=>32,
            'name'=>'Cézár saláta',
            'description'=>'Klasszikus Cézár saláta szardellafilével.',
            'packing'=>'1 adag',
            'price'=>3000,
            'is_food'=>'1',
            'image_id'=>9,
            'category_id'=>9
            ] );
            
            
                        
            Product::create( [
            'id'=>33,
            'name'=>'Jázmin rizs',
            'description'=>'',
            'packing'=>'1 adag',
            'price'=>1000,
            'is_food'=>'1',
            'image_id'=>33,
            'category_id'=>10
            ] );
            
            
                        
            Product::create( [
            'id'=>34,
            'name'=>'Petrezselymes burgonya',
            'description'=>'',
            'packing'=>'1 adag',
            'price'=>1000,
            'is_food'=>'1',
            'image_id'=>54,
            'category_id'=>10
            ] );
            
            
                        
            Product::create( [
            'id'=>35,
            'name'=>'Hasábburgonya',
            'description'=>'',
            'packing'=>'1 adag',
            'price'=>1000,
            'is_food'=>'1',
            'image_id'=>30,
            'category_id'=>10
            ] );
            
            
                        
            Product::create( [
            'id'=>36,
            'name'=>'Nokedli',
            'description'=>'',
            'packing'=>'1 adag',
            'price'=>1000,
            'is_food'=>'1',
            'image_id'=>50,
            'category_id'=>10
            ] );
            
            
                        
            Product::create( [
            'id'=>37,
            'name'=>'Tarhonya',
            'description'=>'',
            'packing'=>'1 adag',
            'price'=>1000,
            'is_food'=>'1',
            'image_id'=>64,
            'category_id'=>10
            ] );
            
            
                        
            Product::create( [
            'id'=>38,
            'name'=>'Csalamádé',
            'description'=>'',
            'packing'=>'1 adag',
            'price'=>1000,
            'is_food'=>'1',
            'image_id'=>14,
            'category_id'=>11
            ] );
            
            
                        
            Product::create( [
            'id'=>39,
            'name'=>'Majonézes kukoricasaláta',
            'description'=>'',
            'packing'=>'1 adag',
            'price'=>1000,
            'is_food'=>'1',
            'image_id'=>42,
            'category_id'=>11
            ] );
            
            
                        
            Product::create( [
            'id'=>40,
            'name'=>'Burgonyasaláta',
            'description'=>'',
            'packing'=>'1 adag',
            'price'=>1000,
            'is_food'=>'1',
            'image_id'=>6,
            'category_id'=>11
            ] );
            
            
                        
            Product::create( [
            'id'=>41,
            'name'=>'Tavaszi saláta',
            'description'=>'',
            'packing'=>'1 adag',
            'price'=>1000,
            'is_food'=>'1',
            'image_id'=>65,
            'category_id'=>11
            ] );
            
            
                        
            Product::create( [
            'id'=>42,
            'name'=>'Uborkasaláta',
            'description'=>'',
            'packing'=>'1 adag',
            'price'=>1000,
            'is_food'=>'1',
            'image_id'=>72,
            'category_id'=>11
            ] );
            
            
                        
            Product::create( [
            'id'=>43,
            'name'=>'Somlói galuska',
            'description'=>'',
            'packing'=>'1 adag',
            'price'=>2000,
            'is_food'=>'1',
            'image_id'=>56,
            'category_id'=>12
            ] );
            
            
                        
            Product::create( [
            'id'=>44,
            'name'=>'Palacsinta',
            'description'=>'',
            'packing'=>'1 adag',
            'price'=>2000,
            'is_food'=>'1',
            'image_id'=>52,
            'category_id'=>12
            ] );
            
            
                        
            Product::create( [
            'id'=>45,
            'name'=>'Gesztenyepüré',
            'description'=>'',
            'packing'=>'1 adag',
            'price'=>2000,
            'is_food'=>'1',
            'image_id'=>26,
            'category_id'=>12
            ] );
            
            
                        
            Product::create( [
            'id'=>46,
            'name'=>'Csokis muffin',
            'description'=>'',
            'packing'=>'1 adag',
            'price'=>2000,
            'is_food'=>'1',
            'image_id'=>18,
            'category_id'=>12
            ] );
            
            
                        
            Product::create( [
            'id'=>47,
            'name'=>'Coca-cola',
            'description'=>'',
            'packing'=>'0,5 l',
            'price'=>500,
            'is_food'=>'0',
            'image_id'=>11,
            'category_id'=>13
            ] );
            
            
                        
            Product::create( [
            'id'=>48,
            'name'=>'Fanta',
            'description'=>'',
            'packing'=>'0,5 l',
            'price'=>500,
            'is_food'=>'0',
            'image_id'=>22,
            'category_id'=>13
            ] );
            
            
                        
            Product::create( [
            'id'=>49,
            'name'=>'Sprite',
            'description'=>'',
            'packing'=>'0,5 l',
            'price'=>500,
            'is_food'=>'0',
            'image_id'=>58,
            'category_id'=>13
            ] );
            
            
                        
            Product::create( [
            'id'=>50,
            'name'=>'Lipton Ice Tea',
            'description'=>'',
            'packing'=>'0,5 l',
            'price'=>500,
            'is_food'=>'0',
            'image_id'=>39,
            'category_id'=>13
            ] );
            
            
                        
            Product::create( [
            'id'=>51,
            'name'=>'Espresso',
            'description'=>'',
            'packing'=>'0,03 l',
            'price'=>300,
            'is_food'=>'0',
            'image_id'=>21,
            'category_id'=>14
            ] );
            
            
                        
            Product::create( [
            'id'=>52,
            'name'=>'Cappuccino',
            'description'=>'',
            'packing'=>'0,09 l',
            'price'=>900,
            'is_food'=>'0',
            'image_id'=>8,
            'category_id'=>14
            ] );
            
            
                        
            Product::create( [
            'id'=>53,
            'name'=>'Latte Machiato',
            'description'=>'',
            'packing'=>'0,3 l',
            'price'=>900,
            'is_food'=>'0',
            'image_id'=>38,
            'category_id'=>14
            ] );
            
            
                        
            Product::create( [
            'id'=>54,
            'name'=>'Jeges kávé',
            'description'=>'',
            'packing'=>'0,3 l',
            'price'=>900,
            'is_food'=>'0',
            'image_id'=>34,
            'category_id'=>14
            ] );
            
            
                        
            Product::create( [
            'id'=>55,
            'name'=>'Coronita',
            'description'=>'',
            'packing'=>'0,33 l',
            'price'=>660,
            'is_food'=>'0',
            'image_id'=>13,
            'category_id'=>15
            ] );
            
            
                        
            Product::create( [
            'id'=>56,
            'name'=>'Dreher',
            'description'=>'',
            'packing'=>'0,5 l',
            'price'=>1000,
            'is_food'=>'0',
            'image_id'=>19,
            'category_id'=>15
            ] );
            
            
                        
            Product::create( [
            'id'=>57,
            'name'=>'Soproni',
            'description'=>'',
            'packing'=>'0,5 l',
            'price'=>1000,
            'is_food'=>'0',
            'image_id'=>57,
            'category_id'=>15
            ] );
            
            
                        
            Product::create( [
            'id'=>58,
            'name'=>'Heineken',
            'description'=>'',
            'packing'=>'0,5 l',
            'price'=>1000,
            'is_food'=>'0',
            'image_id'=>31,
            'category_id'=>15
            ] );
            
            
                        
            Product::create( [
            'id'=>59,
            'name'=>'Egri bikavér',
            'description'=>'',
            'packing'=>'0,1 l',
            'price'=>500,
            'is_food'=>'0',
            'image_id'=>20,
            'category_id'=>16
            ] );
            
            
                        
            Product::create( [
            'id'=>60,
            'name'=>'Cabernet Sauvignon',
            'description'=>'',
            'packing'=>'0,1 l',
            'price'=>500,
            'is_food'=>'0',
            'image_id'=>7,
            'category_id'=>16
            ] );
            
            
                        
            Product::create( [
            'id'=>61,
            'name'=>'Olaszrizling',
            'description'=>'',
            'packing'=>'0,1 l',
            'price'=>500,
            'is_food'=>'0',
            'image_id'=>51,
            'category_id'=>16
            ] );
            
            
                        
            Product::create( [
            'id'=>62,
            'name'=>'Cserszegi fűszeres',
            'description'=>'',
            'packing'=>'0,1 l',
            'price'=>500,
            'is_food'=>'0',
            'image_id'=>15,
            'category_id'=>16
            ] );
            
            
                        
            Product::create( [
            'id'=>63,
            'name'=>'Johnnie Walker whiskey',
            'description'=>'',
            'packing'=>'0,05 l',
            'price'=>850,
            'is_food'=>'0',
            'image_id'=>35,
            'category_id'=>17
            ] );
            
            
                        
            Product::create( [
            'id'=>64,
            'name'=>'Baileys likőr',
            'description'=>'',
            'packing'=>'0,05 l',
            'price'=>700,
            'is_food'=>'0',
            'image_id'=>1,
            'category_id'=>17
            ] );
            
            
                        
            Product::create( [
            'id'=>65,
            'name'=>'Barackpálinka',
            'description'=>'',
            'packing'=>'0,05 l',
            'price'=>650,
            'is_food'=>'0',
            'image_id'=>2,
            'category_id'=>17
            ] );
            
            
                        
            Product::create( [
            'id'=>66,
            'name'=>'Tequila',
            'description'=>'',
            'packing'=>'0,05 l',
            'price'=>750,
            'is_food'=>'0',
            'image_id'=>67,
            'category_id'=>17
            ] );
            
            
                        
            Product::create( [
            'id'=>67,
            'name'=>'Martini Asti',
            'description'=>'',
            'packing'=>'0,1 l',
            'price'=>800,
            'is_food'=>'0',
            'image_id'=>46,
            'category_id'=>18
            ] );
            
            
                        
            Product::create( [
            'id'=>68,
            'name'=>'Törley Gála Sec',
            'description'=>'',
            'packing'=>'0,1 l',
            'price'=>400,
            'is_food'=>'0',
            'image_id'=>70,
            'category_id'=>18
            ] );
            
            
                        
            Product::create( [
            'id'=>69,
            'name'=>'Lipton Yellow Label',
            'description'=>'',
            'packing'=>'0,2 l',
            'price'=>320,
            'is_food'=>'0',
            'image_id'=>40,
            'category_id'=>19
            ] );
            
            
                        
            Product::create( [
            'id'=>70,
            'name'=>'Gyümölcstea',
            'description'=>'',
            'packing'=>'0,2 l',
            'price'=>320,
            'is_food'=>'0',
            'image_id'=>28,
            'category_id'=>19
            ] );
            
            
                        
            Product::create( [
            'id'=>71,
            'name'=>'Mojito',
            'description'=>'fehér rum, cukorszirup, lime, mentalevél, ásványvíz, zúzott jég',
            'packing'=>'0,25 l',
            'price'=>1750,
            'is_food'=>'0',
            'image_id'=>49,
            'category_id'=>20
            ] );
            
            
                        
            Product::create( [
            'id'=>72,
            'name'=>'Margarita',
            'description'=>'Tequila, Cointreau, citromlé',
            'packing'=>'0,1 l',
            'price'=>1200,
            'is_food'=>'0',
            'image_id'=>43,
            'category_id'=>20
            ] );
    }
}
