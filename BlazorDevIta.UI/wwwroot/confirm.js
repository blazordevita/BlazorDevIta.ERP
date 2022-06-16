var myModal = {};

export function showConfirm(id) {
    myModal[id] = new bootstrap.Modal('#' + id);
    myModal[id].show();
}

export function hideConfirm (id) {
    myModal[id].hide();
}
