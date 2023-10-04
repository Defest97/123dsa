
const FieldNameSelect = document.getElementById('FieldName');
const resultText = document.getElementById('fieldName');
FieldNameSelect.addEventListener('change', function () {
    const selectedValue = FieldNameSelect.value;

    switch (selectedValue) {
        case 'Name':
            resultText.textContent = 'Name: ';
            break;
        case 'FullName':
            resultText.textContent = 'FullName: ';
            break;
        default:
            resultText.textContent = 'Name: ';
            break;
    }
});
