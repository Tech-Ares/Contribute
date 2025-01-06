
String? getUserInitialsByName(String? name) {
  String initials = '';
  if (name?.isNotEmpty ?? false) {
    initials += name![0].toUpperCase();
  }
  return initials.trim();
}