from pyrekordbox import Rekordbox6Database, show_config
from pyrekordbox.anlz import AnlzFile, is_anlz_file
import os
import sys
import pyrekordbox

def dump(obj):
	attrs = vars(obj)
	print(', '.join("%s: %s" % item for item in attrs.items()))

def main():
	print("rekordbox config:")
	show_config()
	app_dir = pyrekordbox.config.get_pioneer_app_dir()
	db = Rekordbox6Database()
	db_content = db.get_content()
	song = db_content[0]
	print("song info:")
	dump(song)


	dat_path = os.path.join(app_dir, "rekordbox", "share", song.AnalysisDataPath.strip("/").replace("/", "\\"))
	ext_path = dat_path.replace(".DAT", ".EXT")
	print(f"dat_path path: {dat_path}")
	print(f"ext_path path: {ext_path}")
	
	if not is_anlz_file(dat_path):
		raise Exception("dat file is not a valid anlz file")

	if not is_anlz_file(ext_path):
		raise Exception("ext file is not a valid anlz file")

	dat = AnlzFile.parse_file(dat_path)
	ext = AnlzFile.parse_file(ext_path)
	print(f"dat contents: {dat}")
	print(f"ext contents: {ext}")

	song_structure = ext.getall("PSSI")[0]
	print(f"mood: {song_structure.mood}, bank: {song_structure.bank}, entries: {song_structure.len_entries}")
	for i in song_structure.entries:
		print(f"index: {i.index}, start: {i.beat}, kind: {i.kind}")

if __name__ == "__main__":
	main()