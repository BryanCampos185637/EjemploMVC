$(function () {
    llamarTabla();
})
function llamarTabla() {
    pintarTabla('/Categoria/lstCategoria', ['id', 'Nombre'], ['IDCATEGORIA', 'NOMBRE'],
        'IDCATEGORIA', 'myTable', true, true);
}
function guardar() {
    if (validarCamposVacios()) {
        var frm = new FormData();
        capturarDatos(frm);
        enviarDatosAlControlador('/categoria/guardarCategoria', frm, undefined, undefined, function () {
            limpiarFormulario();
            llamarTabla();
        });
    } else {
        alert('Complete los campos marcados');
    }
}
function editar(id) {
    llenarFormulario('/categoria/obtenerCategoriaPorId?id=' + id, ['IDCATEGORIA', 'NOMBRE']);
}
function eliminar(id) {
    if (confirm('Estas seguro que deseas eliminar esta categoria?') == 1) {
        eliminarRegistro('/categoria/eliminarCategoria?id=' + id, 'Categoria eliminada', function () {
            llamarTabla();
        });
    }
}