@foreach($images as $image)
    <img src="data:image/png;base64,{{ $image['img_data'] }}" alt="{{ $image['img_name'] }}">
@endforeach
