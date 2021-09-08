export function Replace(el) {
  
    const nativeElement = el.nativeElement;
   
    const parentElement = nativeElement.parentElement;
  
    while (nativeElement.firstChild) {
        parentElement.insertBefore(nativeElement.firstChild, nativeElement);
    }
  
    parentElement.removeChild(nativeElement);
}