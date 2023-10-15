const fieldName = document.querySelector('#fieldName');
const filter = document.querySelector('#filter');
fieldName.addEventListener('click', () => {
    filter.focus();
})
function goToPage() {
    var Page = document.getElementById('Page').value;
    var PageSize = document.getElementById('PageSize').value;
    // Ви можете використовувати skipValue для побудови URL
    var url = `?skip=${(Page-1)*PageSize}`;

    // Перенаправити на новий URL
    window.location.href = url;
}