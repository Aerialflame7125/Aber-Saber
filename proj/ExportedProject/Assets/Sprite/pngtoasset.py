import os
import struct
import argparse
from PIL import Image
import io


class AssetConverter:
    """Converts PNG images to a custom asset format."""
    
    # Asset file signature and version
    SIGNATURE = b'ASSET'
    VERSION = 1
    
    def __init__(self):
        """Initialize the converter."""
        pass
        
    def convert_png_to_asset(self, png_path, asset_path=None):
        """
        Convert a PNG file to the asset format.
        
        Args:
            png_path (str): Path to the PNG file
            asset_path (str, optional): Output path for the asset file. If None, uses the same path with .asset extension
        
        Returns:
            str: Path to the created asset file
        """
        # Determine output path if not specified
        if asset_path is None:
            asset_path = os.path.splitext(png_path)[0] + '.asset'
            
        # Load the PNG using PIL
        try:
            img = Image.open(png_path)
        except Exception as e:
            raise ValueError(f"Could not open PNG file: {e}")
            
        # Extract image properties
        width, height = img.size
        mode = img.mode
        
        # Convert image to binary data
        img_bytes = io.BytesIO()
        img.save(img_bytes, format='PNG')
        img_data = img_bytes.getvalue()
        data_size = len(img_data)
        
        # Get original filename for metadata
        filename = os.path.basename(png_path)
        
        # Create the asset file
        with open(asset_path, 'wb') as f:
            # Write header
            f.write(self.SIGNATURE)
            f.write(struct.pack('<I', self.VERSION))
            
            # Write image properties
            f.write(struct.pack('<II', width, height))
            f.write(struct.pack('<B', len(mode)))
            f.write(mode.encode('utf-8'))
            
            # Write filename
            f.write(struct.pack('<B', len(filename)))
            f.write(filename.encode('utf-8'))
            
            # Write image data size and data
            f.write(struct.pack('<I', data_size))
            f.write(img_data)
            
        return asset_path
        
    def convert_asset_to_png(self, asset_path, png_path=None):
        """
        Convert an asset file back to PNG.
        
        Args:
            asset_path (str): Path to the asset file
            png_path (str, optional): Output path for the PNG file. If None, uses the same path with .png extension
            
        Returns:
            str: Path to the created PNG file
        """
        # Determine output path if not specified
        if png_path is None:
            png_path = os.path.splitext(asset_path)[0] + '.png'
            
        # Read the asset file
        with open(asset_path, 'rb') as f:
            # Verify signature
            signature = f.read(len(self.SIGNATURE))
            if signature != self.SIGNATURE:
                raise ValueError("Invalid asset file signature")
                
            # Read version
            version, = struct.unpack('<I', f.read(4))
            if version != self.VERSION:
                raise ValueError(f"Unsupported asset version: {version}")
                
            # Read image properties
            width, height = struct.unpack('<II', f.read(8))
            
            # Read mode
            mode_length, = struct.unpack('<B', f.read(1))
            mode = f.read(mode_length).decode('utf-8')
            
            # Read original filename (we don't use it here, but we need to skip it)
            filename_length, = struct.unpack('<B', f.read(1))
            _ = f.read(filename_length).decode('utf-8')
            
            # Read image data
            data_size, = struct.unpack('<I', f.read(4))
            img_data = f.read(data_size)
            
        # Create PIL image from data
        img = Image.open(io.BytesIO(img_data))
        img.save(png_path)
        
        return png_path


def main():
    """Command line interface for the converter."""
    parser = argparse.ArgumentParser(description='Convert between PNG and asset files')
    parser.add_argument('input_file', help='Input file path')
    parser.add_argument('-o', '--output', help='Output file path (optional)')
    parser.add_argument('--to-png', action='store_true', help='Convert from asset to PNG (default is PNG to asset)')
    
    args = parser.parse_args()
    converter = AssetConverter()
    
    try:
        if args.to_png:
            output_path = converter.convert_asset_to_png(args.input_file, args.output)
            print(f"Successfully converted asset to PNG: {output_path}")
        else:
            output_path = converter.convert_png_to_asset(args.input_file, args.output)
            print(f"Successfully converted PNG to asset: {output_path}")
    except Exception as e:
        print(f"Error: {e}")
        return 1
        
    return 0


if __name__ == "__main__":
    exit(main())