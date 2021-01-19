window.onload = function () {
    llenarComboCategoria();
    llamarTabla();
}
function llamarTabla() {
    pintarTabla('/producto/listarProducto', ['id', 'nombre', 'precio'],
        ['IDPRODUCTO', 'DESCRIPCION', 'PRECIO'], 'IDPRODUCTO', undefined, true, true);
}
function llenarComboCategoria() {
    llenarSelect('/categoria/lstCategoria', undefined, 'IDCATEGORIA', 'NOMBRE', 'IDCATEGORIA')
}
function guardar() {
    if (validarCamposVacios()) {
        if (validarSiEsNumero(document.getElementById('PRECIO'))) {
            var frm = new FormData();
            capturarDatos(frm);
            frm.append('IMAGEN', document.getElementById('IMAGEN').src);
            enviarDatosAlControlador('/producto/guardarProducto', frm, undefined,'Este producto ya esta registrado', function () {
                limpiarFormulario();
                llamarTabla();
                document.getElementById("IMAGEN").src = '';
            });
        } else {
            alert('Precio debe ser numerico')
        }
    } else {
        alert('Complete los campos marcados');
    }
}
function editar(id) {
    llenarComboCategoria();
    llenarFormulario('/producto/obtenerProductoPorId?id=' + id,
        ['IDPRODUCTO', 'DESCRIPCION', 'PRECIO', 'IMAGEN', 'IDCATEGORIA']);
}
function eliminar(id) {
    if (confirm('Estas seguro que deseas eliminar este producto?') == 1) {
        eliminarRegistro('/producto/eliminarProducto?id=' + id, 'Producto eliminado', function () {
            llamarTabla();
        });
    }
}
document.getElementById('txtFileFoto').onchange = function () {
    var file = document.getElementById("txtFileFoto").files[0];//capturamos el archivo
    var reader = new FileReader();//leemos el archivo
    if (reader != null) {//verificamos que no sea null
        reader.onloadend = function () {//cuando termine de cargar
            var img = document.getElementById("IMAGEN");//capturamos el tag de la foto
            img.src = reader.result;//y le asignamos la ruta
        }
        reader.readAsDataURL(file);//convierte a base64
    }
};
document.getElementById('btnFoto').onclick = function () {
    document.getElementById('txtFileFoto').click();
}