   M S B A M L   `   `   `  ����F  BlibraryCrud, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:libraryCrudlibraryCrud  5   	       libraryCrud.MainWindow   5   	   W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:libraryCrud     5   	   -   5          � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5      �  � RowDefinitions 5   
   �� 5      6   �� 5      6   �� 5      $��4* �6   6*   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   $��Center=�6X   .+�5      �� $��16(�6   $
��Bold"�6"   $��0,0,20,0q�64   .��6G   Author6O   �� 5      -   5        $��cb_auth��5      $	��180��6&   $��	0,0,120,0q�62   6n   �� 5      $��16(�6   $
��Bold"�6"   $��0,0,20,0q�64   .��6F   
Category6P   �� 5      -   5        $��cb_catg��5      $	��180��6&   6[   5      �� 5   
   $��1��6   $ 
Horizontal=�6"   $��Center=�6;   $��Center=�6X   .+�5      �� -   5        $��16(�5      $	��180��6#   $
��Bold"�6/   $��0,0,60,0q�6A   .��6h   Add6m   �� 5      -   5        $��16(�5      $	��180��6#   $
��Bold"�6/   $��0,0,60,0q�6A   .��6j   Update6r   �� 5      -   5        $��16(�5      $	��180��6#   $
��Bold"�6/   $��0,0,0,0q�6A   .��6i   Delete6q   5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��dg_books��5      $��2��6#    � 	Resources 5      =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value��%�   '        �� 5      "  6   .�5      ��  � ��
 ���@�6G   �� 5        � ��$ SemiBold"�63   6F   �� 5!       � ��
 �   �6D   �� 5"       � ��$ 0,0,1,2q�68   6J   5#      5$      5%      5&      5'      