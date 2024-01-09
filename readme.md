Ha azt akarjátok, hogy az index lekérésekor ne képek, hanem a képek kódja jöjjön választként, akkor ezzel a két sorral variáljatok az ImageController-ben:

        //Ez adja vissza a képek kódját:
        return response()->json($encodedImages);
        
        //Ez adja vissza a képeket:
        return view('images.index', ['images' => $encodedImages]);

Most úgy van beállítva, hogy a képeket adja vissza, mert meg akartam nézni, hogy működik-e a encode-decode...
